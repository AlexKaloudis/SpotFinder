import { useState, useEffect } from 'react';
import { fetchSomeData } from '../service/apiService';

const TestData = () => {
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
        {data ? (
            <div>
            {/* Render the fetched data */}
            <h1>{data[0].name}</h1>
            <p>{data[0].description}</p>
            </div>
        ) : (
            <p>Loading...</p>
        )}
        </>
    )
}

export default TestData;