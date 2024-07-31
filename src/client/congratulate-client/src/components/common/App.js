import { Route, Routes, Navigate } from 'react-router-dom';
import SharedLayout from './SharedLayout';
import Home from '../../pages/Home';
import List from '../../pages/List';
import { useEffect } from 'react';

function App() {
  useEffect(() => {
    document.title = "Поздравлятор"
  }, []);

  return (
    <Routes>
      <Route path="/" element={<SharedLayout />}>
        <Route index element={<Home />} />
        <Route path="/list" element={<List />} />
        <Route path="*" element={<Navigate to="/"/>} />
      </Route>
    </Routes>
  );
}

export default App;
