import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import RoomList from "./components/RoomList";
import RoomDetail from "./components/RoomDetail";

const AppRoutes = [
    {
        path: '/rooms',
        element: <RoomDetail />

    }
];

export default AppRoutes;
