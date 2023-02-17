import React, { Component } from 'react'
import axios from 'axios';
export default class departmentView extends Component {
    state = {
        departments: [],
    }
    componentDidMount() {
        console.log(localStorage.getItem("AdminAuth"));
        if (localStorage.getItem("AdminAuth") === "false") {
            window.location = "AdminLogin";
        }
        else {
            const token = localStorage.getItem("token");
            axios.get(`https://localhost:44368/api/Department/departments`, { headers: { "Authorization": `Bearer ${token}` } })
                .then(res => {
                    const departments = res.data;
                    console.log(departments);
                    this.setState({ departments });
                })
                .catch(error => {
                    console.log(error);
                    alert("error occurred");
                })
        }
    }
    render() {
        return (
            <div className='row'>
                <div className='col-2'></div>
                <div className='col-8'>
                    <hr />
                    <h1 className="text-center"><q>List of All Departments</q></h1>
                    <hr />
                    <table className="table table-striped table-hover table-bordered text-center">
                        <thead>
                            <tr>
                                <th scope="col-1">Sno #</th>
                                <th scope="col-2">Departments ID</th>
                                <th scope="col-2">Departments Name</th>
                                <th scope="col-7">No Of Employees</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.departments
                                    .map((department, index) =>
                                        <tr key={index}>
                                            <th scope="row">{index + 1}</th>
                                            <td>{department.id}</td>
                                            <td>{department.name}</td>
                                            <td>{department.noOfEmp}</td>
                                        </tr>
                                    )
                            }
                        </tbody>
                    </table>
                </div>
                <div className='col-2'></div>
            </div>
        )
    }
}
