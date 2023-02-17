import React from 'react'
import axios from 'axios';
//import PropTypes from 'prop-types';



export default class EmployeeEdit extends React.Component {

  constructor() {
    super()
    this.state = {
        id: '',
        name: '',
        contact: '',
        email: '',
        departmentId: '',
      }
    }
      handleChangeI = event => {
        //this.setState({ id: event.target.value });
        alert("Can not edit ID");
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
    
        //this.setState({ departmentId: event.target.value });
        alert("Department Can not be edit.");
      }
       
      
    componentDidMount(){
      //console.log("dani", history)
      //console.log(this.props.params);
      //console.log(this.props, "Params");
      //this.paramsID();
        
      console.log(localStorage.getItem("AdminAuth"));
        if(localStorage.getItem("AdminAuth") === "false"){
            window.location = "AdminLogin";
        }
        else{
          const token = localStorage.getItem("token");
          const empId = window.location.pathname.split('/')[2];
          // console.log(window.location, "Location");
          // console.log(window.navigator, "Nav"); 
          // console.log(window.history, "History"); 
          // console.log(window.history.state.usr.from, "History usr"); 
          //console.log(this.props.match,"MAtch");

          console.log(empId);     
              axios.get(`https://localhost:44368/api/Employee/employee-detail?id=${empId}`,{ headers: {"Authorization" : `Bearer ${token}`} })
                .then(res => {
                    console.log(res);
                    console.log(res.data);
                    this.setState({id: res.data.id});
                    this.setState({name: res.data.name});
                    this.setState({contact: res.data.contact});
                    this.setState({email: res.data.email});
                    this.setState({departmentId: res.data.departmentId});
                    console.log(this.state.id);
                    
                    //alert("Employee id " + this.state.id + " has beenc Edited Successfull");
                    
                  })
                .catch(error => {
                    console.log(error);
                })
        }
    }
    handleReset = event => {
        event.target.value = "";
    }

    handleSubmit = event => {
        event.preventDefault();
        
        const token = localStorage.getItem("token");
        
        
        const user = {
            id: this.state.id,
            name: this.state.name,
            contact: this.state.contact,
            email: this.state.email,
            departmentId: this.state.departmentId,
        };
        // this.setState({id: '',
        //             name: '',
        //             contact: '',
        //             email: '',
        //             departmentId: ''});
        axios.post(`https://localhost:44368/api/Employee/edit-employee`, user,{ headers: {"Authorization" : `Bearer ${token}`} })
                .then(res => {
                    console.log(res);
                    console.log(res.data);
                    alert("Employee id " + this.state.id + " has beenc Edited Successfull");
                    
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
          <h1>Edit Employee:</h1><hr />
          <form onSubmit={this.handleSubmit} onReset={this.handleReset}>
            <div className='form-group '>
            <label>
                ID:
                <input type="text" required value={this.state.id} name="id" onChange={this.handleChangeI} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
              </label> <br /><br />
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
              <button type="submit" className="btn btn-primary mx-2">Edit Employee</button>
            </div>
          </form>
          <br />
        </div>
        <div className='col-3'></div>
      </div>
    )
  }
}
//export default withRouter(employeeEdit)
// employeeEdit.propTypes = {
//   od: PropTypes.number,
//   Home: PropTypes.string,
//   About: PropTypes.string

// }