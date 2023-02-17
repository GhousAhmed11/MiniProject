import React, { useState, useEffect } from 'react'
import { useParams, useLocation } from 'react-router-dom'
import axios from 'axios'

export default function EmployeeEditF() {
    let [id, setid] = useState("");
    let [name, setName] = useState("");
    let [contact, setContact] = useState("");
    let [email, setEmail] = useState("");
    let [departmentId, setDepartmentId] = useState("");
    let paramid = useParams();
    let loc = useLocation();
    console.log(loc,"Location");

    const handleSubmit = (event) => {
        event.preventDefault();

        const token = localStorage.getItem("token");


        const user = {
            id: id,
            name: name,
            contact: contact,
            email: email,
            departmentId: departmentId,
        };

        axios.post(`https://localhost:44368/api/Employee/edit-employee`, user, { headers: { "Authorization": `Bearer ${token}` } })
            .then(res => {
                console.log(res);
                console.log(res.data);
                alert("Employee id " + id + " has beenc Edited Successfull");

            })
            .catch(error => {
                console.log(error);
                alert("error occurred");
            })
    }



    useEffect(() => {
        const i = paramid.id
        console.log(i)
        console.log(localStorage.getItem("AdminAuth"));
        if (localStorage.getItem("AdminAuth") === "false") {
            window.location = "/AdminLogin";
        }
        else {
            const token = localStorage.getItem("token");
            axios.get(`https://localhost:44368/api/Employee/employee-detail?id=${i}`, { headers: { "Authorization": `Bearer ${token}` } })
                .then(res => {
                    //console.log(res);
                    console.log(res.data);
                    setid(res.data.id);
                    setName(res.data.name);
                    setContact(res.data.contact);
                    setEmail(res.data.email);
                    setDepartmentId(res.data.departmentId);
                    console.log(this.state.id);

                    //alert("Employee id " + this.state.id + " has beenc Edited Successfull");

                })
                .catch(error => {
                    console.log(error);
                })
        }

    }, [paramid]);
    const handleChangeI = (event) => {
        //setid(event.target.value);
        alert("Can not change ID");
    }
    const handleChangeN = (event) => {
        setName(event.target.value);
    }
    const handleChangeC = (event) => {
        setContact(event.target.value);

    }
    const handleChangeE = (event) => {
        setEmail(event.target.value);
    }
    const handleChangeD = (event) => {
        setDepartmentId(event.target.value);
    }
    return (
        <div className='row'>
            <div className='col-3'></div>
            <div className='col-6'>
                <h1>Edit Employee:</h1><hr />
                <form onSubmit={handleSubmit} >
                    <div className='form-group '>
                        <label>
                            ID:
                            <input type="text" required value={id} name="id" onChange={handleChangeI} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                        </label> <br /><br />
                        <label>
                            Name:
                            <input type="text" required value={name} name="name" onChange={handleChangeN} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                        </label> <br /><br />
                        <label>
                            Contact:
                            <input type="text" required value={contact} name="contact" onChange={handleChangeC} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                        </label> <br /><br />
                        <label>
                            Email:
                            <input type="text" required value={email} name="email" onChange={handleChangeE} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
                        </label> <br /><br />
                        <label>
                            Department ID:
                            <input type="number" required value={departmentId} name="departmentId" onChange={handleChangeD} className="form-control" aria-label="Default" aria-describedby="inputGroup-sizing-default" />
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
