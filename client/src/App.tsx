import { ListItem, ListItemText, Typography } from "@mui/material";
import axios from "axios";
import { useState, useEffect } from "react";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios.get<Activity[]>("https://localhost:5001/api/activities")
      .then(response => setActivities(response.data))
  }, []);

  return (
    <>
      <Typography variant="h2" component="h1" gutterBottom>
        Mactivities
      </Typography>
      <ul>
        {activities.map((activity) => (
          <ListItem key={activity.id}>
            <ListItemText>{activity.title}</ListItemText>
          </ListItem>
        ))}
      </ul>
    </>
  );
}

export default App;
