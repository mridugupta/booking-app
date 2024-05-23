import React, { useState, useEffect } from 'react';
import axios from 'axios';

const RoomList = () => {
    const [rooms, setRooms] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:7106/api/rooms')
            .then(response => {
                setRooms(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the rooms!', error);
            });
    }, []);

    return (
        <div>
            <h1>Available Rooms</h1>
            <ul>
                {rooms.map(room => (
                    <li key={room.id}>
                        <h2>{room.title}</h2>
                        <p>{room.description}</p>
                        <p>${room.price} per night</p>
                        <a href={`/room/${room.id}`}>View Details</a>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default RoomList;