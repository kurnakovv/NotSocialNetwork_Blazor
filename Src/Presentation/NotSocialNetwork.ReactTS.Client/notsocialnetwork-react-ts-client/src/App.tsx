import React from 'react';

import './App.css';
import { ShortPublication } from './components/Publication/ShortPublication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import AppContext from "./contexts/AppContext";
import { useDispatch, useSelector } from 'react-redux';
import { getPublications } from './redux/actions/publications';

const App: React.FC = ({}) => {
  const [eventWindowIsVisible, setEventWindowIsVisible] = React.useState<boolean>(false);
  const [eventWindowText, setEventWindowText] = React.useState<string>("");
  const [eventWindowImage, setEventWindowImage] = React.useState<string>("");

  const dispatch = useDispatch();
  const publications: any = useSelector((state: any) => state.publications.publications);

  React.useEffect(() => {
    dispatch(getPublications());
  }, []);

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
          publications && publications.map((publication: any) => {
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
