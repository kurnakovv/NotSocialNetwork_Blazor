import React from 'react';
import './App.css';
import { ShortPublication } from './components/Publication/ShortPublication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import FavoriteImg from "./img/favorite.png";
import AppContext from "./contexts/AppContext";

const App: React.FC = ({}) => {
  const [publications, setPublications] = React.useState<any[]>([]);
  const [eventWindowIsVisible, setEventWindowIsVisible] = React.useState<boolean>(false);
  const [eventWindowText, setEventWindowText] = React.useState<string>("");
  const [eventWindowImage, setEventWindowImage] = React.useState<string>("");

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
        eventWindowText: eventWindowText,
        setEventWindowText: setEventWindowText,
        eventWindowImg: eventWindowImage,
        setEventWindowImg: setEventWindowImage,
      }}>
      <div className="App">
        <header className="App-header">
        <Navbar />
        <EventWindow />
        <ShortPublication />
        <ShortPublication />
        <ShortPublication />
        </header>
      </div>
      </AppContext.Provider>
    );
}

export default App;
