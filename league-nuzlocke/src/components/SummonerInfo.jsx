import React, { useState } from 'react';
import axios from 'axios';

const SummonerInfo = () => {
  const [summonerName, setSummonerName] = useState('');
  const [tagline, setTagline] = useState('');
  const [summonerInfo, setSummonerInfo] = useState(null);
  const [error, setError] = useState(null);

  const fetchSummonerInfo = async () => {
    try {
      const response = await axios.get('http://localhost:5122/api/riot/summoner', {
        params: {
          name: summonerName,
          tagline: tagline,
        },
      });
      setSummonerInfo(response.data);
      setError(null);
    } catch (err) {
      setError(err.response ? err.response.data : 'Error fetching data');
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    fetchSummonerInfo();
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Summoner Name:</label>
          <input
            type="text"
            value={summonerName}
            onChange={(e) => setSummonerName(e.target.value)}
          />
        </div>
        <div>
          <label>Tagline:</label>
          <input
            type="text"
            value={tagline}
            onChange={(e) => setTagline(e.target.value)}
          />
        </div>
        <button type="submit">Fetch Summoner Info</button>
      </form>
      {error && <div style={{ color: 'red' }}>{error}</div>}
      {summonerInfo && (
        <div>
          <h3>Summoner Info:</h3>
          <pre>{JSON.stringify(summonerInfo, null, 2)}</pre>
        </div>
      )}
    </div>
  );
};

export default SummonerInfo;
