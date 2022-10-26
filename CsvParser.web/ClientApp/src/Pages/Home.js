import React,{useState, useEffect} from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
const Home=()=>{
const history=useHistory();
const[people,setPeople]=useState([]);
const getPeople=async()=>{
    const{data}=await axios.get('/api/csv/getall');
    setPeople(data);
}
useEffect(()=>{
    getPeople();
},[])
const onDeleteClick=async()=>{
    await axios.post('api/csv/deleteall');
    getPeople();
    history.push('/');
}
return(
    <>
    <div className="container">
        <div className="col-md-6 offset-md-3 mt-5">
            <button className="btn btn-block btn-danger" onClick={onDeleteClick}>Delete All</button>
        </div>
        <div className="row-mt-5">
            <table className="table table-stripe table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Fist Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map((p)=>{
                        return(<tr key={p.id}>
                            <td>{p.id}</td>
                            <td>{p.firstName}</td>
                            <td>{p.lastName}</td>
                            <td>{p.age}</td>
                            <td>{p.address}</td>
                            <td>{p.email}</td>
                        </tr>)
                    })}
                </tbody>
            </table>
        </div>
    </div>
    </>
)
}
export default Home;