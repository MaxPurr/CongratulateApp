import axios from 'axios';

const api = axios.create({
  baseURL: "http://localhost:8081/v1/api/people",
});

const peopleApi = {
  getList: async ({orderBy, orderByDescending}) => {
    return api.get(`/list?orderBy=${orderBy}&orderByDescending=${orderByDescending}`);
  },
  getBirthdays: async ({maxDaysLeft, orderBy, orderByDescending}) => {
    return api.get(`/birthdays?maxDaysLeft=${maxDaysLeft}&orderBy=${orderBy}&orderByDescending=${orderByDescending}`);
  },
  add: async (person) => {
    return api.post('/add', person);
  },
  uploadPhoto: async (id, image) => {
    const formData = new FormData();
    formData.append('photo', image);
    return api.post(`/uploadPhoto/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
  },
  delete: async (id) => {
    return api.delete(`/delete/${id}`);
  },
};

export default peopleApi;