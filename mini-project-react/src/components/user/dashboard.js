import React, { Component } from 'react'

export default class dashboard extends Component {
    state = {
        token: localStorage.getItem("token"),
        auth: localStorage.getItem("auth")
    }
    componentDidMount() {
        console.log(localStorage.getItem("auth"));
        if (localStorage.getItem("auth") === "false") {
            window.location = "Login";
        }
    }
    render() {

        return (
            <div className='container'>
                <div className="row">
                    <div className="col-4"></div>
                    <div className="col-4">
                        <h1>Welcome to Employee Dashboard</h1>
                        <h3>{this.state.token}</h3>
                    </div>
                    <div className="col-4"></div>
                </div>
            </div>
        )
    }
}
