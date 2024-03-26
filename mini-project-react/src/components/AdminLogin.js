import React, { Component } from 'react'
import axios from 'axios';

export default class AdminLogin extends Component {
    state = {
        empId: null,
        password: ""
    }
    handleChangeP = event => {
        this.setState({ password: event.target.value });
    }
    componentDidMount() {
        localStorage.clear();
        localStorage.setItem("AdminAuth", false);
        localStorage.setItem("auth", true);
    }

    handleSubmit = event => {
        event.preventDefault();

        const user = {
            empId: this.state.empId,
            password: this.state.password
        };

        console.log('gtyfytfytf');

        axios.post(`https://localhost:44368/api/AppUser/login`, user)
            .then(res => {
                localStorage.setItem("token", res.data);
                localStorage.setItem("AdminAuth", true);

                console.log(res);
                console.log(res.data);
                window.location = "/AdminDash"
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
                        <h1>Admin Login:</h1>
                        <form onSubmit={this.handleSubmit}>
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
