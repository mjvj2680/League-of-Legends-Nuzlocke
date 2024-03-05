import React, { useEffect, useState } from 'react';

const DataAccess = ({ accountInfo }) => {
  const [puuid, setPuuid] = useState(null);

  const getPuuid = async (summonerName, tagline) => {
    try {
      const response = await fetch(`http://localhost:5122/api/summoner`);
      const data = await response.json();

      if (response.ok) {
        setPuuid(data.puuid);
      } else {
        console.error(`Error fetching summoner PUUID: ${data.message}`);
      }
    } catch (error) {
      console.error('Error fetching summoner PUUID:', error.message);
    }
  };

  useEffect(() => {
    if (accountInfo) {
      getPuuid(accountInfo.summonerName, accountInfo.tagline);
    }
  }, [accountInfo]);

  return (
    <div>
      <h2>Data Access Component</h2>
      <p>Summoner Name: {accountInfo.summonerName}</p>
      <p>Tagline: {accountInfo.tagline}</p>
      {puuid && <p>PUUID: {puuid}</p>}
    </div>
  );
};

export default DataAccess;
