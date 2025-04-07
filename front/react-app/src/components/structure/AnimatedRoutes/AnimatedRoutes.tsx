import { Route, Routes, useLocation } from "react-router-dom";
import ProtectedRoute from "../../../infrastructure/Routes/ProtectedRoute";
import Dashboard from "../../dashboard/Dashboard";
import HailFailed from "../../errors/HailFailed";
import LoginForm from "../../login/LoginForm/LoginForm";
import ContentContainer from "../ContentContainer/ContentContainer";
import { AnimatePresence } from "framer-motion";
import InvoiceSearch from "../../invoice/search/InvoiceSearch";
import InvoiceUpload from "../../invoice/upload/InvoiceUpload";
import ComponentsSearch from "../../common/component/ComponentsSearch";
import ProviderSearch from "../../provider/ProviderSearch";

const AnimatedRoutes = () => {
  const location = useLocation();
  return (
    <AnimatePresence>
      <Routes location={location} key={location.pathname}>
        <Route path="/login" element={<LoginForm />} />
        <Route element={<ProtectedRoute />}>
          <Route
            path="/"
            element={
              <ContentContainer>
                <Dashboard />
              </ContentContainer>
            }
          ></Route>
          <Route
            path="/components-search"
            element={
              <ContentContainer>
                <ComponentsSearch />
              </ContentContainer>
            }
          />
          <Route
            path="/invoice-search"
            element={
              <ContentContainer>
                <InvoiceSearch></InvoiceSearch>
              </ContentContainer>
            }
          />
          <Route
            path="/invoice-upload"
            element={
              <ContentContainer>
                <InvoiceUpload />
              </ContentContainer>
            }
          />
          <Route
            path="/providers-search"
            element={
              <ContentContainer>
                <ProviderSearch />
              </ContentContainer>
            }
          />
          <Route
            path="/kurac"
            element={
              <ContentContainer>
                <HailFailed />
              </ContentContainer>
            }
          />
        </Route>
        <Route path="/hailFailed" element={<HailFailed />} />
      </Routes>
    </AnimatePresence>
  );
};

export default AnimatedRoutes;
