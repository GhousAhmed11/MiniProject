import React, { Component } from 'react'
import { Link } from "react-router-dom";

export default class usernav extends Component {
    handleLogout = () => {
        localStorage.clear();
        window.location = "Login";
    }

    render() {
        return (
            <nav className="navbar navbar-expand-lg bg-light">
                <div className="container-fluid">
                    <Link className="navbar-brand" to="Dashboard">Home</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        k   <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">

                            <li className="nav-item">
                                <Link className="nav-link" to="Dashboard">Dashboard</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="View">My Details</Link>
                            </li>



                        </ul>
                        <button className="btn btn-outline-success" type="submit" onClick={this.handleLogout}>Logout</button>
                    </div>
                </div>
            </nav>
        )


    }
}
