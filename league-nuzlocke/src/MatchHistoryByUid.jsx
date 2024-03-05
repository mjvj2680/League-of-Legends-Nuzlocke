import React from 'react';
import DataAccess from './dataaccess/DataAccess.jsx';

const MatchHistoryByUid = () => {
  // Mock accountInfo data for testing purposes
  const accountInfo = {
    summonerName: 'TrojahnPower',
    tagline: '#EUW',
  };

  return (
    <div>
      <DataAccess accountInfo={accountInfo} />
      <p>asaddsa</p>
    </div>
  );
};

export default MatchHistoryByUid;
