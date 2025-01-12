const API_CONFIG = {
    BASE_URL: process.env.REACT_APP_API_URL || 'http://localhost:5273',
    ENDPOINTS: {
        LOGIN: '/api/auth/login',
        REGISTER: '/api/auth/register'
    }
};

export default API_CONFIG; 