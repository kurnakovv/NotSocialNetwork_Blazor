import React from 'react';
import './App.css';
import { ShortPublication } from './components/Publication/ShortPublication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import FavoriteImg from "./img/favorite.png";

const App: React.FC = ({}) => {
  const [publications, setPublications] = React.useState<any[]>([]);

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
    );
}

export default App;
