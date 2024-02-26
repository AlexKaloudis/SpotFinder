import { useState, useEffect } from 'react';
import { fetchSomeData } from '../service/apiService';
import { IconButton, Typography } from "@material-tailwind/react";
import { ArrowRightIcon, ArrowLeftIcon } from "@heroicons/react/24/outline";
import '../styles/SpotStyles.css';

const Spots = () => {
    const [data, setData] = useState(null);
    const [active, setActive] = useState(1);
    const [totalCount, setTotalCount] = useState(0); // Add this line
    const pageSize = 6; // Set your page size here

    const fetchData = async () => {
        try {
          const result = await fetchSomeData(active - 1, pageSize); // active - 1 because your API expects a 0-based index
          setData(result.items);
          setTotalCount(result.totalCount); // Add this line
        } catch (error) {
          console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        fetchData();
    }, [active]);

    const handleNext = () => {
        setActive(prevActive => prevActive + 1);
    };

    const handlePrevious = () => {
        setActive(prevActive => Math.max(prevActive - 1, 1)); // Ensure the page index doesn't go below 1
    };

    const totalPages = Math.ceil(Math.abs(totalCount / pageSize)); // Calculate the total number of pages

    return (
        <>
          <div className="wrapper">
              {data ? (
                  data.map(d => (
                    <div className='mini-wrapper' key={d.id}>
                          <p>{d.title}</p>
                          <a href={d.link} className="inline-flex items-center px-3 py-2 text-sm font-medium text-center text-white bg-blue-700 rounded-lg hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                              Book a Ticket
                              <svg className="rtl:rotate-180 w-3.5 h-3.5 ms-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
                                  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 5h12m0 0L9 1m4 4L9 9"/>
                              </svg>
                          </a>
                    </div>
                  )) 
                ) : (
                    <p>Loading...</p>
                )}
          </div>

          <div className="flex items-center gap-8">
            <IconButton
              size="sm"
              variant="outlined"
              onClick={handlePrevious}
              disabled={active === 1}
            >
                <ArrowLeftIcon strokeWidth={2} className="h-4 w-4" />
            </IconButton>
            {totalCount && pageSize ? (
                <Typography color="gray" className="font-normal">
                    Page <strong className="text-gray-900">{active}</strong> of{" "}
                    <strong className="text-gray-900">{totalPages}</strong>
                </Typography>
            ) : (
                <p>Loading...</p>
            )}
            <IconButton
              size="sm"
              variant="outlined"
              onClick={handleNext}
              disabled={active === totalPages}
            >
              <ArrowRightIcon strokeWidth={2} className="h-4 w-4" />
            </IconButton>
          </div>
        </>
    )
}

export default Spots;
