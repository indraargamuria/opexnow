import { useEffect, useState } from "react";

function App() {
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    fetch("/api/weatherforecast")
      .then(res => res.json())
      .then(setData)
      .catch(err => console.error(err));
  }, []);

  return (
    <div>
      <h1>Weather Data</h1>
      <ul>
        {data.map((item, i) => (
          <li key={i}>
            {item.date} - {item.temperatureC}°C - {item.summary}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;