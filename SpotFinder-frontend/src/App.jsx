import { Routes, Route} from "react-router-dom";
import Home from "../src/components/Home";
import About from "../src/components/About";
import NavbarDefault from "../src/components/NavbarDefault";
import Error404 from "../src/components/Error404";
import Contact from "../src/components/Contact";
import TestData from "./components/TestData";
import FooterComponent from "./components/FooterComponent";
import Spots from "./components/Spots";

function App() {

  return (
    <>
    <NavbarDefault/>
    <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/about" element={<About />} />
        <Route path="/spots" element={<Spots />} />
        <Route path="*" element={<Error404 />} />
    </Routes>
    <FooterComponent/>
    </>
  )
}

export default App
