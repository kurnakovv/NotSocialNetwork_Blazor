import React from 'react';

import './App.css';
import { ShortPublication } from './components/Publication/ShortPublication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import FavoriteImg from "./img/favorite.png";
import AppContext from "./contexts/AppContext";
import axios from 'axios';

const App: React.FC = ({}) => {
  const [publications, setPublications] = React.useState<any[]>([]);
  const [eventWindowIsVisible, setEventWindowIsVisible] = React.useState<boolean>(false);
  const [eventWindowText, setEventWindowText] = React.useState<string>("");
  const [eventWindowImage, setEventWindowImage] = React.useState<string>("");

  React.useEffect(() => {
    async function getPublications() {
      try {
        const result = await axios.get("https://localhost:5001/api/publication/index=0");

        setPublications(result.data);
      } catch (error) {
        console.log(error);
      }
    }

    getPublications();
  }, [])

    return (
      <AppContext.Provider value={{
        eventWindowIsVisible: eventWindowIsVisible,
        setEventWindowIsVisible: setEventWindowIsVisible,
        eventWindowText: eventWindowText,
        setEventWindowText: setEventWindowText,
        eventWindowImg: eventWindowImage,
        setEventWindowImg: setEventWindowImage,
      }}>
      <div className="App">
        <header className="App-header">
        <Navbar />
        <EventWindow />
        {
          publications.map((publication: any) => {
            return (
              <ShortPublication 
                key={publication.id} 
                text={publication.text}
                author={publication.author}
              />
            )
          })
        }
        </header>
      </div>
      </AppContext.Provider>
    );
}

export default App;
