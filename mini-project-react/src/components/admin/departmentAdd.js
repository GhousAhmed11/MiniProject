import React, { Component } from 'react'
import axios from 'axios';


export default class departmentAdd extends Component {

  state = {
    name: '',
  }
  handleChangeT = event => {
    this.setState({ name: event.target.value });
  }
  componentDidMount() {
    console.log(localStorage.getItem("AdminAuth"));
    if (localStorage.getItem("AdminAuth") === "false") {
      window.location = "AdminLogin";
    }
  }
  handleSubmit = event => {
    event.preventDefault();
    this.setState({
      name: '',
    })
    const token = localStorage.getItem("token")
    const user = {
      name: this.state.name,
    };

    axios.post(`https://localhost:44368/api/Department/department`, user, { headers: { "Authorization": `Bearer ${token}` } })
      .then(res => {
        console.log(res);
        console.log(res.data);
        alert("Department Add Successfull");
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
          <h1>Add Department:</h1><hr />
          <form onSubmit={this.handleSubmit}>
            <div className='form-group '>
              <label>
                Department:
                <input type="text" required value={this.state.name} name="title" onChange={this.handleChangeT} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
              </label> <br /><br />
              <button type="submit" className="btn btn-primary mx-2">Add Department</button>
            </div>
          </form>
          <br />
        </div>
        <div className='col-3'></div>
      </div>
    )
  }
}
