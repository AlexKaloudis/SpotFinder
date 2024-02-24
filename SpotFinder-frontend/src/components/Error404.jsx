import { NavLink } from "react-router-dom";

const Error404 = () =>{
    return (
        <>
        <div>
            <h1>404 — Page Not Found</h1>
            <NavLink to="/">Take me back!</NavLink>
        </div>
        </>
    )
}

export default Error404;