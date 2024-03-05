import './App.css';
import React from 'react';
import MatchHistoryByUid from './MatchHistoryByUid';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MatchHistoryByUid />} />
      </Routes>
    </Router>
  );
};

export default App;
