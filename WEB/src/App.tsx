import { Route, Routes } from "react-router";
import Home from "./pages/Home";
import MainLayout from "./layouts/MainLayout";
import "./styles/main.css"
import NotFound from "./pages/NotFound";
import CarShiftOpen from "./pages/carShift/Open";

export default function App() {
    return (
        <Routes>
            <Route element={<MainLayout />}>
                <Route index element={<Home />} />
                <Route path="*" element={<NotFound />} />
                <Route path="/shifts/cars/open" element={<CarShiftOpen />} />
            </Route>
        </Routes>
    );
}