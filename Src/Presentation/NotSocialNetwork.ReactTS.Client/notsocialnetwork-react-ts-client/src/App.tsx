import React from 'react';
import './App.css';
import { ShortPublication } from './components/Publication/ShortPublication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import FavoriteImg from "./img/favorite.png";
import AppContext from "./contexts/AppContext";

const App: React.FC = ({}) => {
  const [publications, setPublications] = React.useState<any[]>([]);
  const [eventWindowIsVisible, setEventWindowIsVisible] = React.useState(false);

  // TODO: Connect ajax + connect data.
  // React.useEffect(() => {
  //   function fetchPublications() {
  //     fetch("https://localhost:5001/api/publication/index=0", {
  //           "method": "GET"
  //     }).then(response => response.json())
  //       .then(response => {
  //         setPublications(prevState => ({
  //           ...prevState,
  //           publications: response,
  //       }))
  //   }

  //   })

    return (
      <AppContext.Provider value={{
        eventWindowIsVisible: eventWindowIsVisible,
        setEventWindowIsVisible: setEventWindowIsVisible,
      }}>
      <div className="App">
        <header className="App-header">
        <Navbar />
        <EventWindow 
          img={FavoriteImg}
        />
        <ShortPublication />
        <ShortPublication />
        <ShortPublication />
        </header>
      </div>
      </AppContext.Provider>
    );
}

export default App;
