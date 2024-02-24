import image1 from '../assets/tech-event1.jpg'; // replace with your image path


const Image = () => {
    return (
      <img
      className="h-96 w-full rounded-lg object-cover object-center"
      src={image1}
      alt="nature image"
    />
    )
}

export default Image;