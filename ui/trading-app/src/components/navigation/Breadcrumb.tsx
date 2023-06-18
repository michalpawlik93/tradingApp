import React from "react";
import { useMatches, RouteObject } from "react-router-dom";
import { Breadcrumbs as MuiBreadcrumbs, Theme } from "@mui/material";
import { Interpolation } from "@emotion/react";

interface BreadcrumbsProps {
  css?: Interpolation<Theme>;
}
export const Breadcrumbs = ({ css }: BreadcrumbsProps): React.ReactElement => {
  const matches: RouteObject[] = useMatches();
  const crumbs = matches
    .filter((match) => Boolean(match.handle?.crumb))
    .map((match) => match.handle.crumb(match.path));

  return (
    <MuiBreadcrumbs css={css}>
      {crumbs.map((crumb, index) => (
        <div key={index}>{crumb}</div>
      ))}
    </MuiBreadcrumbs>
  );
};
