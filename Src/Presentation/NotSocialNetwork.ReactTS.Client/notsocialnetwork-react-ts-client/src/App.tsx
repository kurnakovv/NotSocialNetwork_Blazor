import React from 'react';
import './App.css';
import { GetPublications } from './components/Publication/GetPublications';
import { ShortPublication } from './components/Publication/ShortPublication';
import { Navbar } from './components/Navbar';

class App extends React.Component<any, any> {
    constructor(props: any) {
      super(props);
      this.state = {
          publications: [],
      }
  };
  componentDidMount() {
      fetch("https://localhost:5001/api/publication/index=0", {
          "method": "GET"
      })
      .then(response => response.json())
      .then(response => {
        this.setState({
          publications: response
        })
      })
      .catch(err => { console.log(err) })
  }

  render(){
    return (
      <div className="App">
        <header className="App-header">
        <Navbar />
        <ShortPublication />
        <ShortPublication />
        <ShortPublication />
          {/* <GetPublications 
            publications={this.state.publications}
          /> */}
        </header>
      </div>
    );
  }
}

export default App;
