import axios from 'axios';
import React, { Component } from 'react'

export default class view extends Component {

    state = {
        id: '',
        name: '',
        email: '',
        contact: '',
        createdDate: ''

    }
    componentDidMount() {
        if (localStorage.getItem("auth") === "false") {
            window.location = "Login";
        }
        else {
            const token = localStorage.getItem("token");
            console.log(token);
            axios.post(`https://localhost:44368/api/AppUser/decode?token=${token}`)
                .then(res => {
                    const val = res.data;
                    console.log(val);
                    console.log(val.empId);
                    axios.get(`https://localhost:44368/api/Employee/employee-detail?id=${val.empId}`, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(res => {
                            const person = res.data;
                            console.log(person);
                            this.setState({ id: person.id })
                            this.setState({ name: person.name })
                            this.setState({ email: person.email })
                            this.setState({ contact: person.contact })
                            this.setState({ createdDate: person.createdDate })
                        })
                        .catch(error => {
                            console.log(error);
                            alert("error occurred");
                        })
                    console.log(res);
                    console.log(res.data);
                })
                .catch(error => {
                    console.log(error);
                    alert("error occurred");
                })
        }
    }
    render() {
        return (
            <div className='container'>
                <h1>Employee Details</h1>
                <ul className="list-group ">
                    <li className="list-group-item text-center"><b>Id :</b> {this.state.id}</li>
                    <li className="list-group-item text-center"><b>Name :</b> {this.state.name}</li>
                    <li className="list-group-item text-center"><b>Email Address:</b> {this.state.email}</li>
                    <li className="list-group-item text-center"><b>Contact No:</b> {this.state.contact}</li>
                    <li className="list-group-item text-center"><b>Created Date :</b> {this.state.createdDate}</li>
                </ul>
            </div>
        )
    }
}
