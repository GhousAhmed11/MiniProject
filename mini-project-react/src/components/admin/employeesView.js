import React, { Component } from 'react'
import axios from 'axios';
import { Link } from "react-router-dom";
//import EmployeeEdit from './employeeEdit';

export default class employeesView extends Component {
    constructor(props) {
        super(props)
        this.state = {
            employees : [],
        }
    }
      componentDidMount(){
      console.log("dani employee view",this.props);
        console.log(localStorage.getItem("AdminAuth"));
        if(localStorage.getItem("AdminAuth") === "false"){
            window.location = "AdminLogin";
            
        }
        else{
          const token = localStorage.getItem("token");
          axios.get(`https://localhost:44368/api/Employee/employees`, { headers: {"Authorization" : `Bearer ${token}`} })
                            .then(res => {
                                const employees = res.data;
                                console.log(employees);
                                this.setState({employees});
                                
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
            <h1 className="text-center"><q>List of All Employees</q></h1>
            <hr />
            <table className="table table-striped table-hover table-bordered text-center">
                <thead>
                    <tr>
                        <th scope="col-1">Sno #</th>
                        <th scope="col-2">Employee ID</th>
                        <th scope="col-2">Employee Name</th>
                        <th scope="col-7">Contact</th>
                        <th scope="col-7">Email</th>
                        <th scope="col-2">Department Id</th>
                        <th scope="col-7">Created Date</th>
                        <th scope="col-7">Action</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.employees
                            .map((employee, index) =>
                                <tr key={index}>
                                    <th scope="row">{index + 1}</th>
                                    <td>{employee.id}</td>
                                    <td>{employee.name}</td>
                                    <td>{employee.contact}</td>
                                    <td>{employee.email}</td>
                                    <td>{employee.departmentId}</td>
                                    <td>{employee.createdDate}</td>
                                    <td><Link to={`/EmployeeEdit/${employee.id}`} > <b>Edit</b> </Link></td>
                                </tr>
                            )
                    }
                </tbody>
            </table>
            <Link to={"/EmployeeEdit/1002"} state={{ from: "1002" , xd : "21212"}}>asd</Link>
        </div>
        <div className='col-2'></div>
    </div>
    )
  }
}
