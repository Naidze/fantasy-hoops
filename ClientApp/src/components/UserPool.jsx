import React, { Component } from 'react';
import { UserCard } from './Profile/UserCard';
import shortid from 'shortid';
import _ from 'lodash';
import { DebounceInput } from 'react-debounce-input';
import { Loader } from './Loader';

export class UserPool extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loader: true
    }
    this.filterList = this.filterList.bind(this);
  }

  filterList(e) {
    if (this.state.initialUsers) {
      let updatedList = this.state.initialUsers;
      updatedList = _.filter(updatedList, user => {
        return user.userName.toLowerCase().search(e.target.value.toLowerCase()) !== -1
      });
      this.setState({ users: updatedList });
    }
  }

  async componentWillMount() {
    await fetch(`http://68.183.213.191:5001/api/user`)
      .then(res => {
        return res.json()
      })
      .then(res => {
        this.setState({
          initialUsers: res,
          users: res,
          loader: false
        });
      })
  }

  render() {
    if (this.state.loader)
      return <div className="m-5"><Loader show={this.state.loader} /></div>;

    const users = _.map(
      this.state.users,
      (user) => {
        return <UserCard
          key={shortid()}
          user={user}
          color={user.color}
        />
      }
    );

    return (
      <div className="container bg-light pt-4 pb-2">
        <div className="search m-3 mb-4">
          <span className="fa fa-search"></span>
          <DebounceInput
            className="form-control" type="search" name="search" placeholder="Search..."
            debounceTimeout={600}
            onChange={this.filterList} />
        </div>
        <div className="center col">
          <div className="row">
            {users}
          </div>
        </div>
      </div >
    );
  }
}