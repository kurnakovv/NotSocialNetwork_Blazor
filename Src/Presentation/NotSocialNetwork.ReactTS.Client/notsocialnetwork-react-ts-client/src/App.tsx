import React from 'react';

import './App.css';
import { Publication } from './components/Publication';
import { Navbar } from './components/Navbar';
import EventWindow from './components/EventWindow';
import AppContext from "./contexts/AppContext";
import { useDispatch, useSelector } from 'react-redux';
import { getPublications } from './redux/actions/publications';
import IPublication from './types/Publication.interface';
import AuthPanel from "./components/AuthPanel";

const App: React.FC = ({}) => {
  const [eventWindowIsVisible, setEventWindowIsVisible] = React.useState<boolean>(false);
  const [eventWindowText, setEventWindowText] = React.useState<string>("");
  const [eventWindowImage, setEventWindowImage] = React.useState<string>("");

  const dispatch = useDispatch();
  const publications: IPublication[] = useSelector((state: any) => state.publications.publications);
  const isAuthPanelOpen: boolean = useSelector((state: any) => state.authPanel.isAuthPanelOpen);

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
        </header>
        <EventWindow />
        {isAuthPanelOpen && <AuthPanel />}
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
      </div>
      </AppContext.Provider>
    );
}

export default App;
