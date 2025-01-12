const API_CONFIG = {
    BASE_URL: process.env.REACT_APP_API_URL || 'http://localhost:5273',
    ENDPOINTS: {
        LOGIN: '/api/auth/login',
        REGISTER: '/api/auth/register',
        TASKS: '/api/tasks',
        TASK_BY_ID: (id) => `/api/tasks/${id}`,
        COMPLETED_TASKS: '/api/tasks/completed'
    }
};

export default API_CONFIG; 