import React from 'react';

import './App.css';
import { Publication } from './components/Publication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import AppContext from "./contexts/AppContext";
import { useDispatch, useSelector } from 'react-redux';
import { getPublications } from './redux/actions/publications';
import IPublication from './types/Publication.interface';

const App: React.FC = ({}) => {
  const [eventWindowIsVisible, setEventWindowIsVisible] = React.useState<boolean>(false);
  const [eventWindowText, setEventWindowText] = React.useState<string>("");
  const [eventWindowImage, setEventWindowImage] = React.useState<string>("");

  const dispatch = useDispatch();
  const publications: IPublication[] = useSelector((state: any) => state.publications.publications);

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
          publications && publications.map((publication: IPublication) => {
            return (
              <Publication 
                key={publication.id} 
                text={publication.text}
                author={publication.author}
                id={publication.id}
                imagePaths={publication.imagePaths}
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
