import Login from './components/login.js';
import AdminLogin from './components/AdminLogin.js';
import Registration from './components/registration.js';
import Dashboard from './components/user/dashboard.js';
import View from './components/user/view.js';
import UserNav from './components/user/usernav.js';
import AdminDash from './components/admin/adminDash';
import DepartmentAdd from './components/admin/departmentAdd';
import DepartmentView from './components/admin/departmentView';
import EmployeeAdd from './components/admin/employeeAdd';
import EmployeeEdit from './components/admin/employeeEdit';
import EmployeeEditF from './components/admin/EmployeeEditF';
import EmployeesView from './components/admin/employeesView';
import PageNotFound from './components/PageNotFound';
import AdmiNnav from './components/admin/adminnav.js';
//import history from '../src/util/history';
import Nav from './components/Nav.js';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';

function App() {
  // console.log(localStorage.getItem("auth"),"User");
  // console.log(localStorage.getItem("AdminAuth"),"Admin");
  return (
    <div>
      <Router >
        {localStorage.getItem("auth") === "true" ? <UserNav /> : localStorage.getItem("AdminAuth") === "true" ? <AdmiNnav /> : <Nav />}

        <Routes>
          <Route exac path='/' element={<Login />}></Route>
          <Route exac path='Login' element={<Login />}></Route>
          <Route exac path='AdminLogin' element={<AdminLogin />}></Route>
          <Route exac path='Registration' element={<Registration />}></Route>
          <Route exac path='Dashboard' element={<Dashboard />}></Route>
          <Route exac path='View' element={<View />}></Route>
          <Route exac path='AdminDash' element={<AdminDash />}></Route>
          <Route exac path='DepartmentAdd' element={<DepartmentAdd />}></Route>
          <Route exac path='DepartmentView' element={<DepartmentView />}></Route>
          <Route exac path='EmployeeAdd' element={<EmployeeAdd />}></Route>
          <Route exac path='EmployeeEdit/:id' element={<EmployeeEdit />} />
          <Route exac path='EmployeeEditF/:id' element={<EmployeeEditF />} />
          <Route exac path='EmployeesView' element={<EmployeesView />}></Route>
          <Route path="*" element={<PageNotFound/>} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
