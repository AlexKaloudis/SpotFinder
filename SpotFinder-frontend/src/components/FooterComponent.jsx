import { Footer, FooterCopyright, FooterDivider} from 'flowbite-react';
import '../styles/FooterComponent.css'

const FooterComponent = () => {
  return (
    <Footer container>
      <div className="w-full text-center">
        <div className="w-full justify-between sm:flex sm:items-center sm:justify-between">
        </div>
        <FooterDivider />
        <FooterCopyright href="/" by="SpotFinder™" year={2024} />
      </div>
    </Footer>
  );
}

export default FooterComponent;
