import React, { Component } from 'react'
import {Link } from 'react-router-dom'

export default class PageNotFound extends Component {
  render() {
    return (
        <div>
        <h1>404 Error</h1>
        <h1>Page Not Found</h1>
        <Link to= "/"> Back to Login</Link>
    </div>
    )
  }
}
