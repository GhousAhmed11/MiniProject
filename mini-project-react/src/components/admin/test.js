import React from 'react'
import { useParams } from "react-router-dom";

export default function Test() {
    const as = useParams();
    console.log("Asdasd", as);
  return (
    <div>
    </div>
  )
}
