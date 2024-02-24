import { useState, useEffect } from 'react';
import { fetchSomeData } from '../service/apiService';
import '../styles/SpotStyles.css';

const Spots = () => {
    const [data, setData] = useState(null);

    async function fetchData() {
        try {
          const result = await fetchSomeData();
          setData(result);
          console.log(result);
        } catch (error) {
          console.error('Error fetching data:', error);
        }
      }

      useEffect(() => {
        fetchData();
   }, []);

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
        </>
    )
}

export default Spots;