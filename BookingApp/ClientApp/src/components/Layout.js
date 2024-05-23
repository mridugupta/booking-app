import React, { Component } from 'react';
import { Container } from 'reactstrap';

import RoomList from './RoomList';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <RoomList/>
        <Container tag="main">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
