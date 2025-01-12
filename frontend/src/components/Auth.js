import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import API_CONFIG from '../config/api.js';

function Auth() {
  const navigate = useNavigate();
  const [loginData, setLoginData] = useState({
    tcKimlikNo: '',
    password: ''
  });
  const [loginError, setLoginError] = useState('');
  const [registerError, setRegisterError] = useState('');

  const [registerData, setRegisterData] = useState({
    tcKimlikNo: '',
    firstName: '',
    lastName: '',
    password: ''
  });

  const handleLoginChange = (e) => {
    const { name, value } = e.target;
    setLoginData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleRegisterChange = (e) => {
    const { name, value } = e.target;
    setRegisterData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      setLoginError('');
      const response = await fetch(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.LOGIN}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginData)
      });
      
      if (response.ok) {
        const data = await response.json();
        localStorage.setItem('token', data.token);
        const userInfo = {
          firstName: data.firstName,
          lastName: data.lastName,
          tcKimlikNo: data.tcKimlikNo,
          isAdmin: data.isAdmin
        };
        localStorage.setItem('userInfo', JSON.stringify(userInfo));
        navigate('/home');
      } else {
        const error = await response.json();
        setLoginError(error.message || 'Login failed');
      }
    } catch (error) {
      setLoginError('An error occurred during login');
    }
  };

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      setRegisterError('');
      const response = await fetch(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.REGISTER}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(registerData)
      });
      
      if (response.ok) {
        setLoginData({
          tcKimlikNo: registerData.tcKimlikNo,
          password: registerData.password
        });
        setRegisterData({
          tcKimlikNo: '',
          firstName: '',
          lastName: '',
          password: ''
        });
        setRegisterError('Registration successful! You can now login.');
      } else {
        const error = await response.json();
        setRegisterError(error.message || 'Registration failed');
      }
    } catch (error) {
      setRegisterError('An error occurred during registration');
    }
  };

  return (
    <div className="min-h-screen bg-gray-100 flex flex-col justify-center py-12 sm:px-6 lg:px-8">
      <div className="sm:mx-auto sm:w-full sm:max-w-4xl">
        <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
          Task Management App
        </h2>
      </div>

      <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-4xl">
        <div className="bg-white py-8 px-4 shadow sm:rounded-lg sm:px-10">
          <div className="flex gap-8">
            {/* Login Form */}
            <div className="flex-1">
              <h3 className="text-xl font-bold text-gray-900 mb-6">Sign In</h3>
              {loginError && (
                <div className="mb-4 p-3 rounded-md bg-red-50 border border-red-200">
                  <p className="text-sm text-red-600">{loginError}</p>
                </div>
              )}
              <form className="space-y-6" onSubmit={handleLogin}>
                <div>
                  <label htmlFor="login-tcKimlikNo" className="block text-sm font-medium text-gray-700">
                    TC Kimlik No
                  </label>
                  <div className="mt-1">
                    <input
                      id="login-tcKimlikNo"
                      name="tcKimlikNo"
                      type="text"
                      required
                      maxLength="11"
                      minLength="11"
                      value={loginData.tcKimlikNo}
                      onChange={handleLoginChange}
                      className="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                    />
                  </div>
                </div>

                <div>
                  <label htmlFor="login-password" className="block text-sm font-medium text-gray-700">
                    Password
                  </label>
                  <div className="mt-1">
                    <input
                      id="login-password"
                      name="password"
                      type="password"
                      required
                      value={loginData.password}
                      onChange={handleLoginChange}
                      className="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                    />
                  </div>
                </div>

                <div>
                  <button
                    type="submit"
                    className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                  >
                    Sign in
                  </button>
                </div>
              </form>
            </div>

            <div className="hidden sm:block">
              <div className="h-full w-px bg-gray-200"></div>
            </div>

            {/* Register Form */}
            <div className="flex-1">
              <h3 className="text-xl font-bold text-gray-900 mb-6">Create Account</h3>
              {registerError && (
                <div className={`mb-4 p-3 rounded-md ${registerError.includes('successful') ? 'bg-green-50 border border-green-200' : 'bg-red-50 border border-red-200'}`}>
                  <p className={`text-sm ${registerError.includes('successful') ? 'text-green-600' : 'text-red-600'}`}>{registerError}</p>
                </div>
              )}
              <form className="space-y-6" onSubmit={handleRegister}>
                <div>
                  <label htmlFor="register-tcKimlikNo" className="block text-sm font-medium text-gray-700">
                    TC Kimlik No
                  </label>
                  <div className="mt-1">
                    <input
                      id="register-tcKimlikNo"
                      name="tcKimlikNo"
                      type="text"
                      required
                      maxLength="11"
                      minLength="11"
                      value={registerData.tcKimlikNo}
                      onChange={handleRegisterChange}
                      className="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                    />
                  </div>
                </div>

                <div>
                  <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">
                    First Name
                  </label>
                  <div className="mt-1">
                    <input
                      id="firstName"
                      name="firstName"
                      type="text"
                      required
                      value={registerData.firstName}
                      onChange={handleRegisterChange}
                      className="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                    />
                  </div>
                </div>

                <div>
                  <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">
                    Last Name
                  </label>
                  <div className="mt-1">
                    <input
                      id="lastName"
                      name="lastName"
                      type="text"
                      required
                      value={registerData.lastName}
                      onChange={handleRegisterChange}
                      className="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                    />
                  </div>
                </div>

                <div>
                  <label htmlFor="register-password" className="block text-sm font-medium text-gray-700">
                    Password
                  </label>
                  <div className="mt-1">
                    <input
                      id="register-password"
                      name="password"
                      type="password"
                      required
                      minLength="6"
                      value={registerData.password}
                      onChange={handleRegisterChange}
                      className="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500"
                    />
                  </div>
                </div>

                <div>
                  <button
                    type="submit"
                    className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500"
                  >
                    Register
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Auth; 