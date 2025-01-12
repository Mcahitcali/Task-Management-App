import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Auth from './components/Auth';
import Home from './components/Home';

function App() {
  const isAuthenticated = () => {
    return localStorage.getItem('token') !== null;
  };

  const ProtectedRoute = ({ children }) => {
    if (!isAuthenticated()) {
      return <Navigate to="/auth" />;
    }
    return children;
  };

  const AuthRoute = ({ children }) => {
    if (isAuthenticated()) {
      return <Navigate to="/home" />;
    }
    return children;
  };

  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/auth" element={
            <AuthRoute>
              <Auth />
            </AuthRoute>
          } />
          <Route path="/home" element={
            <ProtectedRoute>
              <Home />
            </ProtectedRoute>
          } />
          <Route path="/" element={
            isAuthenticated() ? <Navigate to="/home" /> : <Navigate to="/auth" />
          } />
        </Routes>
      </div>
    </Router>
  );
}

export default App; 