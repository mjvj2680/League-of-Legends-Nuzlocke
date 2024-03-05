import React, { useState, useEffect } from 'react';

const MatchHistoryByUid = () => {
  const [summonerInfo, setSummonerInfo] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await // Replace your fetch call with this
        fetch('http://localhost:5122/api/summoner')
          .then(response => response.json())
          .then(data => console.log(data))
          .catch(error => console.error('Error:', error));
        
        if (!response.ok) {
          throw new Error(`Error fetching summoner info: ${response.statusText}`);
        }

        const data = await response.json();
        setSummonerInfo(data);
      } catch (error) {
        console.error(error.message);
      }
    };

    fetchData();
  }, []); // The empty dependency array ensures this effect runs once after the initial render

  return (
    <div>
      <h2>Summoner Info</h2>
      {summonerInfo ? (
        <div>
          <p>Name: {summonerInfo.name}</p>
          <p>Level: {summonerInfo.summonerLevel}</p>
          {/* Add other relevant fields */}
        </div>
      ) : (
        <p>Loading...s</p>
      )}
    </div>
  );
};

export default MatchHistoryByUid;
