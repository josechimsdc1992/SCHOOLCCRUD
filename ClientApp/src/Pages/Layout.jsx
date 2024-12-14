import { Outlet, Link } from "react-router-dom";
import Header from "./Header";

const Layout = () => {
  return (
    <>
    <Header/>
    <nav className="navbar navbar-expand-lg navbar-dark fixed-top" id="mainNav"><div className="container">
    <Link className="navbar-brand" to="/">School CRUD</Link>
    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        Menu
        <i className="fas fa-bars ms-1"></i>
    </button>
    <div className="collapse navbar-collapse" id="navbarResponsive">
        <ul className="navbar-nav text-uppercase ms-auto py-4 py-lg-0">
            <li className="nav-item"><Link className="nav-link" to="/student">Students</Link></li>
            <li className="nav-item"><Link className="nav-link" to="/teacher">Teachers</Link></li>
            <li className="nav-item"><Link className="nav-link" to="/grade">grades</Link></li>
        </ul>
    </div>
    </div>
    </nav>

      <Outlet />
    </>
  )
};

export default Layout;