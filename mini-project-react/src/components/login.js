import React, { Component } from 'react'
import axios from 'axios';

export default class login extends Component {

    state = {
        empId: null,
        password: "",
    }
    handleChangeT = event => {
        this.setState({ empId: event.target.value });
    }
    handleChangeP = event => {
        this.setState({ password: event.target.value });
    }

    componentDidMount() {
        localStorage.clear();
        localStorage.setItem("auth", false);
        localStorage.setItem("AdminAuth", false);
    }
    handleSubmit = event => {
        event.preventDefault();
        const user = {
            empId: this.state.empId,
            password: this.state.password
        };
        console.log(user);
        axios.post(`https://localhost:44368/api/AppUser/login`, user)
            .then(res => {
                //localStorage.setItem("auth", this.state.auth);
                localStorage.setItem("token", res.data);
                console.log(res);
                console.log(res.data);
                //console.log(localStorage.getItem("token"));
                //console.log(localStorage.getItem("auth"));
                //this.setState({auth: true});
                //console.log(this.state.auth)
                localStorage.setItem("auth", true);
                window.location = "Dashboard";
                console.log(localStorage.getItem("auth"));
            })
            .catch(error => {
                console.log(error);
                alert("error occurred");
            })
    }
    render() {
        return (
            <div className='container '>
                <div className="row">
                    <div className="col-4"></div>
                    <div className="col-4">
                        <h1>User Login:</h1>
                        <form onSubmit={this.handleSubmit}>
                            <div className="form-outline mb-4">
                                <input type="number" id="form2Example1" className="form-control" onChange={this.handleChangeT} />
                                <label className="form-label" htmlFor="form2Example1">Employee ID</label>
                            </div>

                            <div className="form-outline mb-4">
                                <input type="password" id="form2Example2" className="form-control" onChange={this.handleChangeP} />
                                <label className="form-label" htmlFor="form2Example2">Password</label>
                            </div>
                            <button type="submit" className="btn btn-primary btn-block mb-4">Sign in</button>
                        </form>
                    </div>
                    <div className="col-4"></div>
                </div>
            </div>
        )
    }
}
