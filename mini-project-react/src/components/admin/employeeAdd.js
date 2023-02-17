import React, { Component } from 'react'
import axios from 'axios';

export default class employeeAdd extends Component {
  state = {
    name: '',
    contact: '',
    email: '',
    departmentId: '',
  }
  handleChangeN = event => {
    this.setState({ name: event.target.value });
  }
  handleChangeC = event => {
    this.setState({ contact: event.target.value });
  }
  handleChangeE = event => {
    this.setState({ email: event.target.value });
  }
  handleChangeD = event => {
    this.setState({ departmentId: event.target.value });
  }
  componentDidMount() {
    console.log(localStorage.getItem("AdminAuth"));
    if (localStorage.getItem("AdminAuth") === "false") {
      window.location = "AdminLogin";
    }
  }
  handleReset = event => {
    event.target.value = "";
  }

  handleSubmit = event => {
    event.preventDefault();

    // this.setState({
    //   name: '',
    //   contact: '',
    //   email: '',
    //   departmentId: '',
    // })
    //event.target.reset();
    const token = localStorage.getItem("token")
    const user = {
      name: this.state.name,
      contact: this.state.contact,
      email: this.state.email,
      departmentId: this.state.departmentId,
    };

    axios.post(`https://localhost:44368/api/Employee/employee`, user, { headers: { "Authorization": `Bearer ${token}` } })
      .then(res => {
        console.log(res);
        console.log(res.data);
        alert("Employee Add Successfull");
      })
      .catch(error => {
        console.log(error);
        alert("error occurred");
      })
  }
  render() {
    return (
      <div className='row'>
        <div className='col-3'></div>
        <div className='col-6'>
          <h1>Add Employee:</h1><hr />
          <form onSubmit={this.handleSubmit} onReset={this.handleReset}>
            <div className='form-group '>
              <label>
                Name:
                <input type="text" required value={this.state.name} name="name" onChange={this.handleChangeN} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
              </label> <br /><br />
              <label>
                Contact:
                <input type="text" required value={this.state.contact} name="contact" onChange={this.handleChangeC} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
              </label> <br /><br />
              <label>
                Email:
                <input type="text" required value={this.state.email} name="email" onChange={this.handleChangeE} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
              </label> <br /><br />
              <label>
                Department ID:
                <input type="number" required value={this.state.departmentId} name="departmentId" onChange={this.handleChangeD} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
              </label> <br /><br />
              <button type="submit" className="btn btn-primary mx-2">Add Employee</button>
            </div>
          </form>
          <br />
        </div>
        <div className='col-3'></div>
      </div>
    )
  }
}
