import React, { Component } from 'react'

import { Link } from "react-router-dom";

export default class adminnav extends Component {

    handleLogout = () => {
        localStorage.clear();
        window.location = "/Login";
    }
    render() {
        return (
            <nav className="navbar navbar-expand-lg bg-light">
                <div className="container-fluid">
                    <Link className="navbar-brand" to="AdminDash">Home</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            <li className="nav-item">
                                <Link className="nav-link" to="AdminDash">Dashboard</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="DepartmentView">View Departments</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="DepartmentAdd">Add Department</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="EmployeeAdd">Add Employee</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="EmployeeEdit">Edit Employee</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="EmployeesView">View Employees</Link>
                            </li>
                        </ul>
                        <button className="btn btn-outline-success" type="submit" onClick={this.handleLogout}>Logout</button>
                    </div>
                </div>
            </nav>
        )
    }
}
