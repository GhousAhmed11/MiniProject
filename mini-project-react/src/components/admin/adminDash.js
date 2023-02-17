import React, { Component } from 'react'

export default class adminDash extends Component {
  state = {
    token: localStorage.getItem("token"),
  }
  componentDidMount() {
    console.log(localStorage.getItem("AdminAuth"));
    if (localStorage.getItem("AdminAuth") === "false") {
      window.location = "AdminLogin";
    }
  }
  render() {
    return (
      <div className='container'>
        <div className="row">
          <div className="col-4"></div>
          <div className="col-4">
            <h1>Welcome to Admin Dashboard</h1>
            <h3>{this.state.token}</h3>
          </div>
          <div className="col-4"></div>
        </div>
      </div>
    )
  }
}
