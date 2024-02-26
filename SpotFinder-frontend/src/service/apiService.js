import axios from 'axios';

const API_BASE_URL = 'https://localhost:7272/api';

const apiService = axios.create({
  baseURL: API_BASE_URL,
});

export const fetchSomeData = async (pageIndex,pageSize) => {
    const response = await apiService.get(`/Spots?pageIndex=${pageIndex}&pageSize=${pageSize}`);
    // console.log(`pageIndex: ${pageIndex}, pageSize: ${pageSize}, totalCount: ${response.data.totalCount}`);
    return response.data;
}

export const getSpotsWithLocation = async (location) => {
  const response = await apiService.get(`/Spots/${location}`);
  return response.data;
}
