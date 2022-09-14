import logo from './logo.svg';
import './App.css';
import { NavBar } from "./components/NavBar.js";
import{ Banner}from "./components/Banner.js";
import{ Skills }from "./components/Skills.js";
import{ Projects }from "./components/Project.js";
import { Contact } from "./components/Contact";
import { Footer } from "./components/Footer";
import 'bootstrap/dist/css/bootstrap.min.css';
function App() {
  return (
    <div className="App">
      <NavBar />
      <Banner/>
      <Skills/>
      <Projects/>
      <Contact/>
      <Footer/>
    </div>
  );
}

export default App;
