import React, { Component } from 'react'
import axios from 'axios';

export default class registration extends Component {

    state = {
        empId: null,
        password: ""
    }

    handleChangeT = event => {
        this.setState({ empId: event.target.value });
    }
    handleChangeP = event => {
        this.setState({ password: event.target.value });
    }

    handleSubmit = event => {
        event.preventDefault();

        const user = {
            empId: this.state.empId,
            password: this.state.password
        };
        console.log(user);
        axios.post(`https://localhost:44368/api/AppUser/register`, user)
            .then(res => {

                console.log(res);
                console.log(res.data);
                alert("User Registered Successfull")
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
                        <h1>Registraion:</h1>
                        <form onSubmit={this.handleSubmit}>
                            <div className="form-outline mb-4">
                                <input type="number" id="form2Example1" className="form-control" onChange={this.handleChangeT} />
                                <label className="form-label" htmlFor="form2Example1">Employee ID</label>
                            </div>

                            <div className="form-outline mb-4">
                                <input type="password" id="form2Example2" className="form-control" onChange={this.handleChangeP} />
                                <label className="form-label" htmlFor="form2Example2">Password</label>
                            </div>
                            <button type="submit" className="btn btn-primary btn-block mb-4">Register</button>
                        </form>
                    </div>
                    <div className="col-4"></div>
                </div>
            </div>
        )
    }
}
