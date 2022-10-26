import React, {useState} from 'react';
import axios from 'axios';

const Generate=()=>{
const [amount, setAmount]=useState(0);
const onButtonClick=async()=>{
    window.location.href =await `/api/csv/generate?amount=${amount}`;
}

return(<>
<div className='row col-md-4 offset-md-5'>
    <input type='text' placeholder="Amount" onChange={e=>setAmount(e.target.value)} name='amount'/>
    <button className='btn btn-primary' onClick={onButtonClick}>Generate</button>
    </div></>

)
}
export default Generate;

