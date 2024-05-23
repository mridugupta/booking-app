import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const RoomDetail = () => {
    const { id } = useParams();
    const [room, setRoom] = useState(null);

    useEffect(() => {
        axios.get(`http://localhost:7106/api/rooms/${id}`)
            .then(response => {
                setRoom(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the room details!', error);
            });
    }, [id]);

    if (!room) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h1>{room.title}</h1>
            <img src={room.imageUrl} alt={room.title} />
            <p>{room.description}</p>
            <p>Price: ${room.price}</p>
            <p>Address: {room.address}</p>
            <p>Capacity: {room.capacity}</p>
            <button onClick={() => bookRoom(room.id)}>Book Now</button>
        </div>
    );

    function bookRoom(roomId) {
        // Implement booking functionality
    }
};

export default RoomDetail;