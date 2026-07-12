import { Route, Routes } from "react-router";
import Home from "./pages/Home";
import MainLayout from "./layouts/MainLayout";
import "./styles/main.css"
import NotFound from "./pages/NotFound";
import CarShiftOpen from "./pages/carShift/Open";
import UsersPage from "./pages/users/Users";
import UserPage from "./pages/users/User";

export default function App() {
    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route index element={<Home />} />
                <Route path="*" element={<NotFound />} />
                <Route path="/shifts/cars/open" element={<CarShiftOpen />} />
                <Route path="/users" element={<UsersPage />} />
                <Route path="/users/:id" element={<UserPage />} />
            </Route>
        </Routes>
    );
}